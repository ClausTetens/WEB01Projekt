using System;
using System.ComponentModel.DataAnnotations;

namespace Voresjazzklub.Models {
    // http://dotnetmentors.com/mvc/how-to-do-custom-validation-using-validationattribute-of-aspnet-mvc.aspx
    public class UserIdValidatorAttribute : ValidationAttribute {
        private string theErrTxt;
        public UserIdValidatorAttribute(string errtxt) {
            theErrTxt = errtxt;
        }

        // kommer på feltniveau og i @Html.ValidationSummary(false, ""), men ikke i @Html.ValidationSummary(true, "")
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            if(value!=null && value.ToString()=="Claus") {
                //return new ValidationResult("Der er fejl i data: " + theErrTxt);
                return ValidationResult.Success;
            } else {
                return ValidationResult.Success;
                //return new ValidationResult("Der er fejl i data: " + theErrTxt);
            }
        }
    }


    //namespace System.Web.Mvc.Html { 
    //https://msdn.microsoft.com/en-us/library/cc668224.aspx
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class UserIdValidatorAttribute1 : ValidationAttribute {
        public UserIdValidatorAttribute1(string errtxt) {
            string a = errtxt;
        }
        public override bool IsValid(object value) { // køres hver gang der tastes i brugerId
            if(value == null)
                return false;
            if(value.GetType() == typeof(string)) {
                return true;
                //return new UsersTableModel().isUserValid((string)value);
                //return ((UsersTableModel)value).isUserValid();
            }

            return false;
        }
    }



    //http://stackoverflow.com/questions/16100300/asp-net-mvc-custom-validation-by-dataannotation
    /*
    public class CombinedMinLengthAttribute : ValidationAttribute {
        public CombinedMinLengthAttribute(int minLength, params string[] propertyNames) {
            this.PropertyNames = propertyNames;
            this.MinLength = minLength;
        }

        public string[] PropertyNames { get; private set; }
        public int MinLength { get; private set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            var properties = this.PropertyNames.Select(validationContext.ObjectType.GetProperty);
            var values = properties.Select(p => p.GetValue(validationContext.ObjectInstance, null)).OfType<string>();
            var totalLength = values.Sum(x => x.Length) + Convert.ToString(value).Length;
            if(totalLength < this.MinLength) {
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }
            return null;
        }
    }*/
}