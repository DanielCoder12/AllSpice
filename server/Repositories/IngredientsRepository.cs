


namespace AllSpice.Repositories;

public class IngredientsRepository
{
    private readonly IDbConnection _db;

    public IngredientsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Ingredient CreateIngredient(Ingredient ingredientData)
    {
        string sql = @"
        INSERT INTO 
        ingredients(name, quantity, recipeId, creatorId)
        VALUES (@name, @quantity, @recipeId, @creatorId);
        SELECT * FROM ingredients WHERE id = LAST_INSERT_ID();";
        Ingredient ingredient = _db.Query<Ingredient>(sql, ingredientData).FirstOrDefault();
        return ingredient;
    }

    internal void DestroyIngredient(int ingredientId)
    {
        string sql = @"DELETE FROM ingredients WHERE id = @ingredientId LIMIT 1;";
        _db.Execute(sql, new { ingredientId });
    }

    internal Ingredient GetIngredientById(int ingredientId)
    {
        string sql = @"SELECT * FROM ingredients WHERE id = @ingredientId;";
        Ingredient ingredient = _db.Query<Ingredient>(sql, new { ingredientId }).FirstOrDefault();
        return ingredient;
    }
}