using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Recruiting.BL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace Recruiting.BL.Validation
{
    public class IsUniqueJobReferenceAttribute : ValidationAttribute
    {
        public new string ErrorMessage { get; set; } = "Reference must be unique";

        public string ReferencePropertyName { get; set; } = "Reference";
        public string JobIdPropertyName { get; set; } = "JobId";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            PropertyInfo referenceProperty= validationContext.ObjectType.GetProperty(ReferencePropertyName);
            PropertyInfo jobIdProperty= validationContext.ObjectType.GetProperty(JobIdPropertyName);

            string reference = referenceProperty.GetValue(validationContext.ObjectInstance, null)?.ToString();
            if (int.TryParse(jobIdProperty.GetValue(validationContext.ObjectInstance, null)?.ToString(), out int jobId)
                    && !String.IsNullOrEmpty(reference))
            {
                var jobService = validationContext.GetService(typeof(IJobService)) as IJobService;
                if (jobService.IsReferenceUnique(jobId, reference))
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult(ErrorMessage);
        }
    }
}
