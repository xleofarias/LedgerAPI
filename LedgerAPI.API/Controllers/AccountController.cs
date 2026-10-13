using Microsoft.AspNetCore.Mvc;
using LedgerAPI.API;
using LedgerAPI.API.DTOs;
using System.Text.RegularExpressions;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly AppDbContext _context;

    public AccountController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAccountAsync([FromBody] AccountDtoCreate accountDto, CancellationToken ct = default)
    {        

        string pattern = "^[0-9]{3}[A-Z]{3}[-]{1}[A-Z]{3}[0-9]{3}$";

        Regex rg = new Regex(pattern);

        if(!rg.IsMatch(accountDto.AccountNumber))
        {
            return BadRequest("Invalid account number format.");
        }

        var account = new Account(accountDto.AccountNumber)
        {   
            AccountHolder = accountDto.AccountHolder
        };

        if(account.AccountNumber == null)
        {
            return BadRequest("Account number is required.");
        }

        await _context.Accounts.AddAsync(account, ct);
        
        await _context.SaveChangesAsync(ct);
        
        return Created();
    }
}