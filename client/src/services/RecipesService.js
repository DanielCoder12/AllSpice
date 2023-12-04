import { AppState } from "../AppState"
import { Recipe } from "../models/Recipe"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class RecipesService {

    async getRecipes() {
        const res = await api.get('api/recipes')
        logger.log(res.data)
        AppState.filteredRecipes = res.data.map(r => new Recipe(r))
        AppState.recipes = res.data.map(r => new Recipe(r))

    }

    async createRecipe(formData) {
        const res = await api.post('api/recipes', formData)
        debugger
        logger.log(res.data)
        if (AppState.filter === 'Favorites') {
            logger.log('one added')
            AppState.recipes.push(new Recipe(res.data))
            return
        }
        if (AppState.filter === 'Home') {
            AppState.recipes.push(new Recipe(res.data))
            return
        }
        logger.log('two added')
        AppState.recipes.push(new Recipe(res.data))
        AppState.filteredRecipes.push(new Recipe(res.data))
    }
    changeFilter(filter) {
        if (filter == 'Home') {
            AppState.filteredRecipes = AppState.recipes
        }
        if (filter == 'My Recipes') {
            AppState.filteredRecipes = AppState.recipes.filter(r => r.creatorId == AppState.account.id)
        }
        if (filter == 'Favorites') {
            const filteredRecipes = []
            const recipes = AppState.recipes

            for (let i = 0; i < AppState.favorites.length; i++) {
                let fav = recipes.find(r => r.id == AppState.favorites[i].id)
                filteredRecipes.push(fav)
            }
            AppState.filteredRecipes = filteredRecipes
        }
        AppState.filter = filter
    }
}

export const recipesService = new RecipesService()