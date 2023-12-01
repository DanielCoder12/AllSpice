namespace AllSpice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientsController : ControllerBase
{

    private readonly Auth0Provider _auth0;
    private readonly IngredientsService _ingredientsService;

    public IngredientsController(Auth0Provider auth0, IngredientsService ingredientsService)
    {
        _auth0 = auth0;
        _ingredientsService = ingredientsService;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Ingredient>> CreateIngredient([FromBody] Ingredient ingredientData)
    {
        try
        {
            Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
            ingredientData.CreatorId = userInfo.Id;
            Ingredient ingredient = _ingredientsService.CreateIngredient(ingredientData, userInfo.Id);
            return ingredient;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize]
    [HttpDelete("{ingredientId}")]
    public async Task<ActionResult<string>> DestroyIngredient(int ingredientId)
    {
        try
        {
            Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
            string message = _ingredientsService.DestroyIngredient(ingredientId, userInfo.Id);
            return Ok(message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}