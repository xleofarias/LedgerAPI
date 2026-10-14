namespace LedgerAPI.API.DTOs;

public class AccountDtoCreate
{
	public string AccountHolder { get; set; } = string.Empty;
	public string AccountNumber { get; set; } = string.Empty;
}
