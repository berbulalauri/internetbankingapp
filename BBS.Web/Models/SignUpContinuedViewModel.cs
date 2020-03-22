using BBS.Models.Constants;
using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BBS.Web.Models
{
    public class SignUpContinuedViewModel : SignUpViewModel
    {
        [Required]
        public bool Sex { get; set; }
        [Required]
        public string PassportId { get; set; }
        [Required]
        public int CityId { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int QuestionId { get; set; }
        [Required]
        public string Answer { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(RegexConstants.RegexForPassword, ErrorMessage = ErrorMessages.InvalidPassword)]
        public string Password { get; set; }

        public static implicit operator User(SignUpContinuedViewModel signUpContinuedViewModel)
        {
            return new User
            {
                FirstName = signUpContinuedViewModel.FirstName,
                UserName = signUpContinuedViewModel.Email,
                NormalizedUserName = signUpContinuedViewModel.Email,
                LatsName = signUpContinuedViewModel.LatsName,
                PassportId = signUpContinuedViewModel.PassportId,
                Phone = signUpContinuedViewModel.Phone,
                Email = signUpContinuedViewModel.Email,
                NormalizedEmail = signUpContinuedViewModel.Email,
                Gender = signUpContinuedViewModel.Sex,
                CityId = signUpContinuedViewModel.CityId,
                Address = signUpContinuedViewModel.Address,
                QuestionId = signUpContinuedViewModel.QuestionId,
                Answer = signUpContinuedViewModel.Answer,
                RegDate = DateTime.Now,
                PasswordHash = signUpContinuedViewModel.Password
            };
        }
    }
}
