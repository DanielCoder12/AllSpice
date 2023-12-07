<template>
    <!-- FIXME FIX IMAGES -->
    <div @click="setActiveRecipe(recipe)" role="button" class="background-img shadow d-flex flex-column
    justify-content-between rounded" :style="{ backgroundImage: `url(${recipe.img})` }">
        <div class="d-flex text-white justify-content-between">
            <div class="d-flex m-3 rounded-pill bg-blur px-3">
                <p class="fs-4 mb-0 py-1 px-2">{{ recipe.category }}</p>
            </div>
            <div v-if="account.id" class=" ">
                <!-- TODO MAKE THIS WORK -->
                <p class="mb-0 px-2 me-2 bg-blur rounded-bottom ">
                    <i @click.stop="unFavorite(recipe.id)" role="button" title="un-favorite"
                        v-if="favorites.filter(f => f.accountId == account.id && f.id == recipe.id) != 0"
                        class="mdi mdi-heart text-danger fs-3"></i>
                    <i @click.stop="favorite(recipe.id)" role="button" title="favorite" v-else
                        class="mdi mdi-heart-outline text-danger fs-3"></i>
                </p>
            </div>
        </div>
        <div class="">
            <div class="bg-blur rounded m-2 px-2 py-1">
                <p class="text-white text-break mb-1 fw-bold">{{ recipe.title }}</p>
                <!-- <p class="text-white mb-0"> {{ recipe.instructions }}</p> -->
                <p class="text-white mb-0 text-break">By: {{ recipe.creator.name }}</p>

            </div>
        </div>

    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Recipe } from '../models/Recipe';
import Pop from '../utils/Pop';
import { favoritesService } from '../services/FavoritesService'
import { logger } from '../utils/Logger';
import { recipesService } from '../services/RecipesService';
import { Modal } from 'bootstrap';
export default {
    props: { recipe: { type: Recipe } },
    setup() {

        return {
            favorites: computed(() => AppState.favorites),
            account: computed(() => AppState.account),
            async setActiveRecipe(recipe) {
                try {
                    recipesService.setActiveRecipe(recipe)
                    Modal.getOrCreateInstance('#RecipeDetailsModal').show()
                } catch (error) {
                    Pop.error(error)
                }
            },
            async unFavorite(recipeId) {
                try {
                    const favorite = this.favorites.find(f => f.id == recipeId)
                    // logger.log(AppState.favorites[0])
                    // logger.log(recipeId)
                    await favoritesService.unFavorite(favorite.favoriteId)
                } catch (error) {
                    Pop.error(error)
                }
            },
            async favorite(recipeId) {
                try {
                    await favoritesService.favorite(recipeId)
                } catch (error) {
                    Pop.error(error)
                }
            }
        }
    }
};
</script>


<style lang="scss" scoped>
.bg-blur {
    background-color: rgba(0, 0, 0, 0.267);
    backdrop-filter: blur(5px);
}

.background-img:hover {
    transform: scale(1.03);
}

.background-img {
    height: 25rem;
    width: 100%;
    transition: .25s;
    object-fit: cover;
    object-position: center;
}
</style>