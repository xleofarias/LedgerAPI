using System.ComponentModel.DataAnnotations;

namespace LedgerAPI.API.DTOs;

public class AccountDtoCreate
{
	[Required]
	[StringLength(100)]
	public string AccountHolder { get; set; } = string.Empty;
}
