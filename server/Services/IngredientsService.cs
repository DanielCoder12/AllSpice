
namespace AllSpice.Services;

public class IngredientsService
{

    private readonly IngredientsRepository _repo;
    private readonly RecipesService _recipesService;
    public IngredientsService(IngredientsRepository repo, RecipesService recipesService)
    {
        _repo = repo;
        _recipesService = recipesService;
    }

    internal Ingredient CreateIngredient(Ingredient ingredientData, string userId)
    {
        Recipe recipe = _recipesService.GetRecipeById(ingredientData.RecipeId);
        if (recipe.CreatorId != userId)
        {
            throw new Exception("You cannot add ingredients to a recipe that isnt yours");
        }
        Ingredient ingredient = _repo.CreateIngredient(ingredientData);
        return ingredient;
    }

    internal Ingredient GetIngredientById(int ingredientId)
    {
        Ingredient ingredient = _repo.GetIngredientById(ingredientId);
        if (ingredient == null)
        {
            throw new Exception($"Invalid id:{ingredientId}");
        }
        return ingredient;
    }
    internal string DestroyIngredient(int ingredientId, string id)
    {
        Ingredient ingredient = GetIngredientById(ingredientId);
        if (ingredient.CreatorId != id)
        {
            throw new Exception("Not Your Ingredient To Destroy");
        }
        _repo.DestroyIngredient(ingredientId);
        return "ingredient destroyed";
    }
}