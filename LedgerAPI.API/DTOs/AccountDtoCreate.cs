using System.ComponentModel.DataAnnotations;

namespace LedgerAPI.API.DTOs;

public class AccountDtoCreate
{
	[Required]
	[StringLength(100, MinimumLength = 10, ErrorMessage = "Account holder name must be between 10 and 100 characters long.")]
	public string AccountHolder { get; set; } = string.Empty;

	[Required]
	[StringLength(13, MinimumLength = 13, ErrorMessage = "Account number must be exactly 13 characters long.")]
	public string AccountNumber { get; set; } = string.Empty;
}
