import { AppState } from "../AppState"
import { Ingredient } from "../models/Ingredient"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class IngredientsService {

    async getIngredients(recipeId) {
        if (!recipeId) {
            return
        }
        AppState.activeIngredients = []
        const res = await api.get(`api/recipes/${recipeId}/ingredients`)
        logger.log('ingredients', res.data)
        AppState.activeIngredients = res.data.map(i => new Ingredient(i))
    }


}

export const ingredientsService = new IngredientsService()