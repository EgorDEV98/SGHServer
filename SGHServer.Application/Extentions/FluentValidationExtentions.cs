using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Validators;

namespace SGHServer.Application.Extentions;

public static class FluentValidationExtentions
{
    public static IRuleBuilderOptions<T, TProperty> IsGuid<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder) {
        return ruleBuilder.SetValidator(new GuidValidator<T,TProperty>());
    }
    
    private class GuidValidator<T,Guid> : PropertyValidator<T,Guid>
    {
        public override bool IsValid(ValidationContext<T> context, Guid value)
        {
            return Regex.IsMatch(value.ToString(), 
                @"(?im)^[{(]?[0-9A-F]{8}[-]?(?:[0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$");
        }

        public string GetDefaultMessageTemplate(string errorCode)
        {
            return $"{errorCode} не является действительным GUID";
        }
        
        public override string Name { get; }
    }
}