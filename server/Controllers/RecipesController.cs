namespace AllSpice.Controllers;


[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{

    private readonly RecipesService _recipesService;
    private readonly Auth0Provider _auth0;

    public RecipesController(RecipesService recipesService, Auth0Provider auth0)
    {
        _recipesService = recipesService;
        _auth0 = auth0;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Recipe>> CreateRecipe([FromBody] Recipe recipeData)
    {
        try
        {
            Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
            recipeData.CreatorId = userInfo.Id;
            Recipe recipe = _recipesService.CreateRecipe(recipeData);
            return Ok(recipe);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }


    }

    [HttpGet]
    public ActionResult<List<Recipe>> GetAllRecipes()
    {
        try
        {
            List<Recipe> recipes = _recipesService.GetAllRecipes();
            return Ok(recipes);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{recipeId}")]
    public ActionResult<Recipe> GetRecipeById(int recipeId)
    {
        try
        {
            Recipe recipe = _recipesService.GetRecipeById(recipeId);
            return recipe;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize]
    [HttpPut("{recipeId}")]
    public async Task<ActionResult<Recipe>> EditRecipe(int recipeId, [FromBody] Recipe recipeData)
    {
        try
        {
            Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
            Recipe recipe = _recipesService.EditRecipe(recipeId, userInfo.Id, recipeData);
            return recipe;

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize]
    [HttpDelete("{recipeId}")]
    public async Task<ActionResult<string>> DestroyRecipe(int recipeId)
    {
        try
        {
            Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
            string Message = _recipesService.DestroyRecipe(recipeId, userInfo.Id);
            return Message;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{recipeId}/ingredients")]
    public ActionResult<List<Ingredient>> GetIngredientsById(int recipeId)
    {
        try
        {
            List<Ingredient> recipes = _recipesService.GetIngredientsById(recipeId);
            return Ok(recipes);

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }
}