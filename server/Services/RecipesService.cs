


namespace AllSpice.Services;

public class RecipesService
{
    RecipesRepository _repo;

    public RecipesService(RecipesRepository repo)
    {
        _repo = repo;
    }

    internal Recipe CreateRecipe(Recipe recipeData)
    {
        Recipe recipe = _repo.CreateRecipe(recipeData);
        return recipe;
    }

    internal string DestroyRecipe(int recipeId, string id)
    {
        Recipe recipe = GetRecipeById(recipeId);
        if (recipe.CreatorId != id)
        {
            throw new Exception("Not Your Recipe");
        }
        _repo.DestroyRecipe(recipeId);
        return $"{recipe.Title} deleted";

    }

    internal Recipe EditRecipe(int recipeId, string id, Recipe recipeData)
    {
        Recipe recipe = GetRecipeById(recipeId);
        if (recipe.CreatorId != id)
        {
            throw new Exception("Not Your Recipe");
        }
        recipe.Instructions = recipeData.Instructions ?? recipe.Instructions;
        Recipe newRecipe = _repo.EditRecipe(recipe);
        return newRecipe;
    }

    internal List<Recipe> GetAllRecipes()
    {
        List<Recipe> recipes = _repo.GetAllRecipes();
        return recipes;
    }

    internal Recipe GetRecipeById(int recipeId)
    {
        Recipe recipe = _repo.GetRecipeById(recipeId);
        if (recipe == null)
        {
            throw new Exception($"invalid Id: {recipeId}");
        }
        return recipe;
    }
}