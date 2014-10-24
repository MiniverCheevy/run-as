using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace RunAsWrapper.Core.Operations.Apps
{
    public class AppMessage : IValidatableObject
    {

        [Required(ErrorMessage =Messages.Required)]
        public string Key { get; set; }

        [Required(ErrorMessage = Messages.Required)]
        public string FullPath { get; set; }

        public string Arguments { get; set; }

        public bool RequiresAdmin { get; set; }

        public bool UrlEncodeArguments { get; set; }

        public AppMessage()
        {
            Arguments = String.Empty;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var result = new ValidationResult[] { };

            if (string.IsNullOrEmpty(FullPath))
                return result;

            if (!File.Exists(FullPath))
                return new ValidationResult[]{new ValidationResult(
                     Messages.FileNotFound, new string[]{"FullPath"})};

            return new ValidationResult[] { };
        }
    }
}
