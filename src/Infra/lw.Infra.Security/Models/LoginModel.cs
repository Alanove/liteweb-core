﻿using lw.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lw.Security.Models;

public class LoginModel: BaseUIModel
{
	[Required]
	[EmailAddress]
	public string Email { get; set; } = default!;

	[Required]
	[DataType(DataType.Password)]
	public string Password { get; set; } = default!;

	public bool RememberMe { get; set; } = false;

	public string ReturnUrl { get; set; } = default!;
}