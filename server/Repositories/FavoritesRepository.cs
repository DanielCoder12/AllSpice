



namespace AllSpice.Repositories;

public class FavoritesRepository
{
    private readonly IDbConnection _db;

    public FavoritesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Favorite CreateFavorite(Favorite favoriteData)
    {
        string sql = @"INSERT INTO 
        favorites (accountId, recipeId)
        VALUES (@accountId, @recipeId);
        SELECT * FROM favorites WHERE id = LAST_INSERT_ID();";
        Favorite favorite = _db.Query<Favorite>(sql, favoriteData).FirstOrDefault();
        return favorite;
    }

    internal void DestroyFavorite(int favoriteId)
    {
        string sql = @"DELETE FROM favorites WHERE id = @favoriteId LIMIT 1;";
        _db.Execute(sql, new { favoriteId });
    }

    internal List<FavoriteRecipes> GetAccFavorites(string accountId)
    {
        string sql = @"SELECT 
        fav.*,
        rep.*,
        acc.*
        FROM favorites fav
        JOIN recipes rep ON rep.creatorId = fav.accountId 
        JOIN accounts acc ON acc.id = rep.creatorId
        WHERE fav.accountId = @accountId;";
        List<FavoriteRecipes> favorites = _db.Query<Favorite, FavoriteRecipes, Account, FavoriteRecipes>(sql, (favorite, favoriteRecipe, profile) =>
        {
            favoriteRecipe.FavoriteId = favorite.Id;
            favoriteRecipe.AccountId = favorite.AccountId;
            favoriteRecipe.Creator = profile;
            return favoriteRecipe;
        }, new { accountId }).ToList();
        return favorites;
    }

    internal Favorite GetFavoriteById(int favoriteId)
    {
        string sql = @"SELECT * FROM favorites WHERE id = @favoriteId;";
        Favorite favorite = _db.Query<Favorite>(sql, new { favoriteId }).FirstOrDefault();
        return favorite;
    }
}