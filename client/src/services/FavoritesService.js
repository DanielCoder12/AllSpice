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

}

export const favoritesService = new FavoritesService()