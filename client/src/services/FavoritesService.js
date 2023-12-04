import { AppState } from "../AppState"
import { Favorite } from "../models/Favorite"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class FavoritesService {

    async getFavorites() {
        const res = await api.get('account/favorites')
        logger.log(res.data)
        AppState.favorites = res.data.map(f => new Favorite(f))
    }

    async unFavorite(favoriteId) {

        await api.delete(`api/favorites/${favoriteId}`)
        AppState.favorites = AppState.favorites.filter(f => f.favoriteId != favoriteId)
    }

    async favorite(recipeId) {
        const res = await api.post('api/favorites', { recipeId })
        // logger.log(res.data)
        // logger.log(AppState.favorites[0])
        const favorite = {
            id: res.data.recipeId,
            favoriteId: res.data.id,
            creatorId: res.data.creatorId,
            accountId: res.data.accountId
        }
        AppState.favorites.push(new Favorite(favorite))
    }

}

export const favoritesService = new FavoritesService()