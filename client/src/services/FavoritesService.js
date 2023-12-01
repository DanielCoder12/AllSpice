import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class FavoritesService {

    async getFavorites() {
        const res = await api.get('account/favorites')
        logger.log(res.data)
    }

}

export const favoritesService = new FavoritesService()