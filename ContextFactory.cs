using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using NDDR.Helpers;

namespace NDDR
{
    internal class ContextFactory 
    {
        private readonly IConfiguration _config;
        private readonly string _connectionstring;
        private DeferralHandler _deferralHandler;

        public ContextFactory()
        {
            _config = ConfigProvider.Config;
            _connectionstring = _config.GetConnectionString("DefaultConnection");
            _deferralHandler = new DeferralHandler();
        }
        public void Execute(string queryString)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(_connectionstring))
                {
                    OracleCommand command = new OracleCommand(queryString, connection);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

            public async Task<ServiceResult> ExecuteNDDRInquiry(string queryString)
            {
            var res = new ServiceResult();
            var inquiryResult = new InquiryDataDTO();
            OracleCommand command = new OracleCommand();
                try
                {
                    using (OracleConnection connection = new OracleConnection(_connectionstring))
                    {
                        command = new OracleCommand(queryString, connection);
                        command.Connection.Open();
                        var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            inquiryResult.PermenantDeferred = reader[0].ToString();
                            inquiryResult.LastBloodDonation = reader[1].ToString();
                            inquiryResult.LastPlasmaDonation = reader[2].ToString();
                            inquiryResult.DeferalEndDate = reader[3].ToString();
                        }
                        
                    }
                    res = HandleInquiryResult(inquiryResult);
                    return res;
                }
                }
                catch (Exception ex)
                {

                res.inquiryResult = InquiryResult.ERR.ToString();
                return res;
                }
            finally
            {
                command.Connection.Close();
            }

            }
        public User FIndUserInquiry(LoginDto userDto)
        {
            var res = new User();
            OracleCommand command = new OracleCommand();
            try
            {
                using (OracleConnection connection = new OracleConnection(_connectionstring))
                {
                    var queryString = "select * from auth  where USERNAME = '" + userDto.username + "' And PASSWORD = '"+ userDto.password + "'";
                    command = new OracleCommand(queryString, connection);
                    command.Connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                           res.UserName = reader[0].ToString();
                           res.PasswordHash = reader[1].ToString();
                        }
                        return res;
                    }
                    else return null;
                }
            }
            catch (Exception e)
            {

                return null;
            }
            finally
            {
                command.Connection.Close();
            }

        }

        private ServiceResult HandleInquiryResult(InquiryDataDTO donorResult)
        {
            var res = new ServiceResult();
            //Permanent Deferral
            if (!string.IsNullOrEmpty(donorResult.PermenantDeferred) && donorResult.PermenantDeferred.ToUpper().Equals("YES") && string.IsNullOrEmpty(donorResult.LastBloodDonation))
                res.inquiryResult = InquiryResult.MAT.ToString();
            //Cross Donation
            else if (!string.IsNullOrEmpty(donorResult.PermenantDeferred) && donorResult.PermenantDeferred.ToUpper().Equals("NO") && (!string.IsNullOrEmpty(donorResult.LastBloodDonation) || !string.IsNullOrEmpty(donorResult.LastPlasmaDonation)))
            {
                var deffDate = _deferralHandler.GetDefferalEndDate(donorResult);

                res.inquiryResult = (deffDate > DateTime.Now) ? InquiryResult.CRO.ToString() : InquiryResult.NOT.ToString();

                res.deferralEndDate = deffDate.ToString("yyyy-MM-dd");

            }
            //Permanent Deferral && Cross Donation
            else if (!string.IsNullOrEmpty(donorResult.PermenantDeferred) && donorResult.PermenantDeferred.ToUpper().Equals("YES") && !string.IsNullOrEmpty(donorResult.LastBloodDonation))
                res.inquiryResult = InquiryResult.DAC.ToString();
            //Not Found
            else res.inquiryResult = InquiryResult.NOT.ToString();
            return res;
        }
    }


  
}
