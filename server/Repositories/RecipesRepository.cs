





namespace AllSpice.Repositories;

public class RecipesRepository
{
    private readonly IDbConnection _db;

    public RecipesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Recipe CreateRecipe(Recipe recipeData)
    {
        string sql = @"
        INSERT INTO 
        recipes (id, title, instructions, img, category, creatorId)
        VALUES (@Id, @Title, @Instructions, @Img, @Category, @creatorId)
        ;
        SELECT * FROM recipes WHERE id = LAST_INSERT_ID()
        ;";
        Recipe recipe = _db.Query<Recipe>(sql, recipeData).FirstOrDefault();
        return recipe;
    }

    internal void DestroyRecipe(int recipeId)
    {
        string sql = @"DELETE FROM recipes WHERE id = @recipeId LIMIT 1;";
        _db.Execute(sql, new { recipeId });

    }

    internal Recipe EditRecipe(Recipe recipe)
    {
        string sql = @"
        UPDATE recipes
        SET
        instructions = @instructions
        WHERE id = @Id
        LIMIT 1
        ;
        SELECT 
        rec.*,
        acc.*
        FROM recipes rec
        JOIN accounts acc ON rec.creatorId = acc.id
        WHERE rec.id = @Id
        ;";
        Recipe newRecipe = _db.Query<Recipe, Account, Recipe>(sql, (rec, acc) =>
        {
            rec.Creator = acc;
            return rec;
        }, recipe).FirstOrDefault();
        return newRecipe;
    }

    internal List<Recipe> GetAllRecipes()
    {
        string sql = @"SELECT 
        rec.*,
        acc.*
        FROM recipes rec
        JOIN accounts acc ON acc.id = rec.creatorId
        ORDER BY rec.Id

        ;";
        List<Recipe> recipes = _db.Query<Recipe, Account, Recipe>(sql, (rec, acc) =>
        {
            rec.Creator = acc;
            return rec;
        }).ToList();
        return recipes;
    }

    internal List<Ingredient> GetIngredientsById(int recipeId)
    {
        string sql = @"
        SELECT * FROM ingredients WHERE recipeId = @recipeId;";

        List<Ingredient> ingredients = _db.Query<Ingredient>(sql, new { recipeId }).ToList();
        return ingredients;
    }

    internal Recipe GetRecipeById(int recipeId)
    {
        string sql = @"
        SELECT 
        rec.*,
        acc.*
        FROM recipes rec
        JOIN accounts acc ON acc.id = rec.creatorId
        WHERE rec.id = @recipeId
        ;";
        Recipe recipe = _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
        {
            recipe.Creator = account;
            return recipe;
        }, new { recipeId }).FirstOrDefault();
        return recipe;
    }
}