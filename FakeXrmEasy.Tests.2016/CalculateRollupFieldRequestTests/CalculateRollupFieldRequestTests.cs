using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Xunit;
using Crm;

using Microsoft.Crm.Sdk.Messages;
using System.Reflection;

namespace FakeXrmEasy.Tests.FakeContextTests.CalculateRollupFieldRequestTests
{
    public class CalculateRollupFieldRequestTests
    {

        [Fact]
        public void When_calculate_rollup_field_request_is_called_an_entity_is_updated()
        {
            var context = new XrmFakedContext();
            context.ProxyTypesAssembly = Assembly.GetExecutingAssembly();
            var service = context.GetFakedOrganizationService();

            var a = new Account()
            {
                Id = Guid.NewGuid()
            };


            var o = new Opportunity
            {
                Id = Guid.NewGuid(),
                CustomerId = a.ToEntityReference()
                
            };

            var testData = new List<Entity> {a, o};
            
            context.Initialize(testData);

            var request = new CalculateRollupFieldRequest()
            {
                Target = a.ToEntityReference(),
                FieldName = "opendeals"
            };

            var response = service.Execute(request);

            var account = (from acc in context.CreateQuery<Account>()
                           where acc.Id == a.Id
                           select acc).FirstOrDefault();

            Assert.Equal((int) account.OpenDeals.Value, 1);


        }
    }
}
