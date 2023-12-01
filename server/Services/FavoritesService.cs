


namespace AllSpice.Services;

public class FavoritesService
{
    private readonly FavoritesRepository _repo;

    public FavoritesService(FavoritesRepository repo)
    {
        _repo = repo;
    }

    internal Favorite CreateFavorite(Favorite favoriteData)
    {
        Favorite favorite = _repo.CreateFavorite(favoriteData);
        return favorite;
    }

    internal Favorite GetFavoriteById(int favoriteId)
    {
        Favorite favorite = _repo.GetFavoriteById(favoriteId);
        if (favorite == null)
        {
            throw new Exception($"Invalid Id:{favoriteId}");
        }
        return favorite;
    }

    internal string DestroyFavorite(int favoriteId, string userId)
    {
        Favorite favorite = GetFavoriteById(favoriteId);
        if (favorite.AccountId != userId)
        {
            throw new Exception("YOU CAN'T DELETE THIS FAVORITE");
        }
        _repo.DestroyFavorite(favoriteId);
        return "favorite deleted";
    }


    internal List<FavoriteRecipes> GetAccFavorites(string id)
    {
        List<FavoriteRecipes> favorites = _repo.GetAccFavorites(id);
        return favorites;
    }
}