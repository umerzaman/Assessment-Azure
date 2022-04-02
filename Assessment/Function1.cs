using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json; 
using Assessment.DataContecxt;
using Assessment.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Assessment.Dto;
using System.Linq;
using Assessment.HelperClasses;
using System.Net;

namespace Assessment
{
    public class Assessment
    {
        private readonly DataContext _dataContext;  
        public Assessment(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task DebitTransaction(TransactionDto transactionDto)
        {
            var acountDetails =await _dataContext.Users.FirstOrDefaultAsync(x=>x.UserId == transactionDto.Id); //Checking account Info or We can check here with user id 
           
            if(acountDetails !=null &&  acountDetails.Account == transactionDto.Account)
            {
                var transaction = await (from wallets in _dataContext.Wallets
                                    where wallets.Balance >= transactionDto.Amount
                                    select new Transaction
                                    {
                                        Account = acountDetails.Account.ToString(),
                                        Direction = TransactionTypes.Debit,
                                        Amount = transactionDto.Amount,
                                        UserId = transactionDto.Id,
                                        CreatedDate = DateTime.Now
                                    }).FirstOrDefaultAsync();
                if(transaction is null)
                {
                    throw new Exception("Insufficient Balance");
                }
                else
                {
                    _dataContext.Add(transaction);
                    _dataContext.SaveChanges();
                } 
                
            }
            else
            {
                throw new Exception("Unable to Fetch right details");
            } 
        }


        [FunctionName("Transaction")]
        public  async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<TransactionDto>(requestBody);
                await DebitTransaction(data);
            }

            catch(Exception ex)
            {
                log.LogError("Something Went Wrong : "+ ex.Message);
                return new OkObjectResult(HttpStatusCode.BadRequest);
            }

            
            return new OkObjectResult(HttpStatusCode.OK);
        }
    }
}
