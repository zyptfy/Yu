﻿using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace Yu.Core.Validators.CustomValidators
{
    /// <summary>
    /// 自定义手机号码验证器
    /// </summary>
    public class PhoneValidator : PropertyValidator
    {
        // 电话号码格式
        private readonly string _phonePattern = "^((13[0-9])|(14[5,7])|(15[0-3,5-9])|(17[0,3,5-8])|(18[0-9])|166|198|199|(147))\\d{8}$";

        public PhoneValidator() : base("手机号码格式错误") { }

        // 验证方法
        protected override bool IsValid(PropertyValidatorContext context)
        {
            var phoneNumber = context.PropertyValue?.ToString();

            // 号码为空，不合格
            if (string.IsNullOrEmpty(phoneNumber))
                return false;

            // 正则验证
            return Regex.IsMatch(phoneNumber, _phonePattern);
        }
    }
}
