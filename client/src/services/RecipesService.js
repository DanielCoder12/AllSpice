import { AppState } from "../AppState"
import { Ingredient } from "../models/Ingredient"
import { Recipe } from "../models/Recipe"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class RecipesService {

    async getRecipes() {
        const res = await api.get('api/recipes')
        // logger.log(res.data)
        AppState.filteredRecipes = res.data.map(r => new Recipe(r))
        AppState.recipes = res.data.map(r => new Recipe(r))

    }

    async createRecipe(formData) {
        const res = await api.post('api/recipes', formData)
        // logger.log(res.data)
        if (AppState.filter === 'Favorites') {
            // logger.log('one added')
            res.data.creator = { name: AppState.account.name }
            AppState.recipes.push(new Recipe(res.data))
            return
        }
        if (AppState.filter === 'Home') {
            // logger.log('this log ran')
            res.data.creator = { name: AppState.account.name }
            AppState.recipes.push(new Recipe(res.data))
            AppState.filteredRecipes = AppState.recipes
            return
        }
        if (AppState.filter === 'My Recipes') {
            // logger.log('two added')
            res.data.creator = { name: AppState.account.name }
            AppState.recipes.push(new Recipe(res.data))
            this.changeFilter('My Recipes')
            return
        }
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
    async setActiveRecipe(recipe) {
        AppState.activeRecipe = {}
        const res = await api.get(`api/recipes/${recipe.id}`)
        AppState.activeRecipe = new Recipe(res.data)
    }

    searchHomePage(search) {
        if (search == '') {
            AppState.filteredRecipes = AppState.recipes
        }
        this.changeFilter(AppState.filter)
        AppState.filteredRecipes = AppState.filteredRecipes.filter(r => r.category.toLowerCase().includes(search.toLowerCase()))
    }

    async deleteRecipe(recipeId) {
        await api.delete(`api/recipes/${recipeId}`)
        AppState.recipes = AppState.recipes.filter(r => r.id != recipeId)
        AppState.filteredRecipes = AppState.filteredRecipes.filter(r => r.id != recipeId)
    }

    async saveInstructions(instructions, recipeId) {
        const newRecipe = await api.put(`api/recipes/${recipeId}`, { instructions })

    }

    async addIngredient(ingredient) {
        const res = await api.post('api/ingredients', ingredient)
        // logger.log('new ingredient', res.data)
        AppState.activeIngredients.push(new Ingredient(res.data))
    }
}

export const recipesService = new RecipesService()