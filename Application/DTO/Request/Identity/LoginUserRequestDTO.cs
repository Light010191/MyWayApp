﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Request.Identity
{
	public class LoginUserRequestDTO
	{
		[EmailAddress]
		[RegularExpression("[^@ \\t\\r\\n]+@[^@ \\t\\r\\n]+\\.[^@ \\t\\r\\n]+", ErrorMessage = "Your Email is nit valid")]
		public string Email { get; set; }

		[Required]
		[RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$", ErrorMessage = "Your password is not valod")]
		[MinLength(8),MaxLength(25)]
		public string Password { get; set; }
	}
}