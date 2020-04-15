using BDCO.Core.Validation;
using BDCO.Domain.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDCO.Domain.Validators.Student
{
    public class AddStudentValidation : IValidationHandler<AddNewStudentCommand>
    {
        public ValidationResult Validate(AddNewStudentCommand command)
        {
            if (command.Mobile == "") 
                return new ValidationResult() { Message = "Mobile number cannot be empty.", Description = "" };
            return new ValidationResult();
        }
    }
}

//throw new InvalidOperationException("Mobile number cannot be empty.");
