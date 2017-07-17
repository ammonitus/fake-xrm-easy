using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using FakeXrmEasy.FakeMessageExecutors;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;

namespace FakeXrmEasy.CalculateRollupFieldExecutor
{
    public class CalculateRollupFieldExecutor : IFakeMessageExecutor
    {
        public bool CanExecute(OrganizationRequest request)
        {
            return request is CalculateRollupFieldRequest;
        }

        public OrganizationResponse Execute(OrganizationRequest request, XrmFakedContext ctx)
        {
            var req = request as CalculateRollupFieldRequest;

            var entityName = req.Target.LogicalName;
            var guid = req.Target.Id;

            var entityToUpdate = new Entity(entityName) { Id = guid};
            entityToUpdate[req.FieldName] = 1;

            var fakedService = ctx.GetFakedOrganizationService();
            fakedService.Update(entityToUpdate);

            return new CalculateRollupFieldResponse();
        }

        public Type GetResponsibleRequestType()
        {
            return typeof(CalculateRollupFieldRequest);
        }
    }
}
