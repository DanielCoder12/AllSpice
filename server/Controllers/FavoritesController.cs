namespace AllSpice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FavoritesController : ControllerBase
{
    private readonly FavoritesService _favoritesService;
    private readonly Auth0Provider _auth0;

    public FavoritesController(FavoritesService favoritesService, Auth0Provider auth0)
    {
        _favoritesService = favoritesService;
        _auth0 = auth0;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Favorite>> CreateFavorite([FromBody] Favorite favoriteData)
    {
        try
        {
            Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
            favoriteData.AccountId = userInfo.Id;
            Favorite favorite = _favoritesService.CreateFavorite(favoriteData);
            return Ok(favorite);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize]
    [HttpDelete("{favoriteId}")]
    public async Task<ActionResult<string>> DestroyFavorite(int favoriteId)
    {
        try
        {
            Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
            string message = _favoritesService.DestroyFavorite(favoriteId, userInfo.Id);
            return Ok(message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }
}