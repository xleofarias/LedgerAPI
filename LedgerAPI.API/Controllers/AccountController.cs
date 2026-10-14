using Microsoft.AspNetCore.Mvc;
using LedgerAPI.API;
using LedgerAPI.API.DTOs;

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
        
        Account account;

        try
        {
            account = new Account(accountDto.AccountNumber)
            {   
                AccountHolder = accountDto.AccountHolder
            };
        } catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        
        await _context.Accounts.AddAsync(account, ct);
            
        await _context.SaveChangesAsync(ct);
            
        return Created();
    }
}
