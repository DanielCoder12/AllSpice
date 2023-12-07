<template>
    <div class="modal fade modal-xl" id="RecipeDetailsModal" tabindex="-1" aria-labelledby="exampleModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div v-if="recipe.id" class="modal-content ">
                <div class=" d-flex modal-height">
                    <!-- FIXME MAKE PICTURE LOOK BETTER -->
                    <div class="picture-style rounded-start" :style="{ backgroundImage: `url(${recipe.img})` }">
                        <div v-if="account.id" class="text-end ">
                            <!-- TODO MAKE THIS WORK -->
                            <p class="mb-0 px-2 me-2 align-items-start w-100 justify-content-end d-flex rounded-bottom ">
                                <i @click="deleteRecipe(recipe.id)" v-if="account.id == recipe.creator.id" role="button"
                                    title="delete recipe"
                                    class="mdi trash-position bg-blur rounded-bottom fs-1 mdi-delete"></i>
                                <i role="button" title="close modal" data-bs-dismiss="modal" aria-label="Close"
                                    class="mdi mdi-close x-position fs-3"></i>
                                <i @click="unFavorite(recipe.id)" role="button" title="un-favorite"
                                    v-if="favorites.filter(f => f.accountId == account.id && f.id == recipe.id) != 0"
                                    class="mdi mdi-heart bg-blur text-danger rounded-bottom fs-1"></i>
                                <i @click="favorite(recipe.id)" role="button" title="favorite" v-else
                                    class="mdi mdi-heart-outline bg-blur rounded-bottom text-danger fs-1"></i>
                            </p>
                        </div>
                    </div>
                    <div class="div-width">
                        <div class="ps-4 pt-4">
                            <div>
                            </div>
                            <div class="d-flex align-items-center">
                                <h2 class="mb-0 text-break width">{{ recipe.title }}</h2>
                                <p class="bg-gray rounded-pill ms-3 me-5 px-2 text-white mb-0">{{ recipe.category }}</p>
                            </div>
                            <p>{{ recipe.creator.name }}'s recipe</p>
                        </div>
                        <div class="d-flex h-75 position ">
                            <ModalCard>
                                <template #title>
                                    <p class="mb-0">Recipe Steps</p>
                                </template>
                                <template #body>
                                    <form v-if="recipe.creatorId == account.id" @submit.prevent="saveInstructions()"
                                        class="text-end height w-100">
                                        <textarea :disabled="isLocked.locked" maxlength="1000" v-model="data.instructions"
                                            class="rounded p-2" name="" id="">{{ recipe.instructions }}</textarea>
                                        <button @click=" unlock()" v-if="isLocked.locked" type="button"
                                            class="btn py-1 btn-secondary">Edit</button>
                                        <button v-else type="submit" class="btn bg-green text-white py-1">Save</button>
                                    </form>
                                    <textarea v-else disabled name="" id=""
                                        class="rounded p-2"> {{ recipe.instructions }}</textarea>
                                </template>
                            </ModalCard>
                            <ModalCard>
                                <template #title>
                                    <p class="mb-0">Ingredients</p>
                                </template>
                                <template #body>
                                    <div v-if="!ingredients[0]?.name">
                                        no ingredients
                                    </div>
                                    <div v-else class="overflow flex-grow-1">
                                        <p class="mb-1 d-flex justify-content-between text-break"
                                            v-for="ingredient in ingredients" :key="ingredient.id">{{
                                                ingredient.quantity }}
                                            {{ ingredient.name }}
                                            <span v-if="recipe.creatorId == account.id" class="text-end text-break"><i
                                                    @click="deleteIngredient(ingredient.id)"
                                                    class="mdi mdi-delete text-danger" title="delete ingredient"
                                                    role="button"></i></span>
                                        </p>
                                    </div>
                                </template>
                                <template #form>
                                    <!-- FIXME MAKE LOOK BETTER -->
                                    <form v-if="recipe.creatorId == account.id" @submit.prevent="addIngredient()"
                                        class="d-flex">
                                        <input maxlength="25" v-model="data.name" type="text" class="form-control"
                                            placeholder="Add Ingredients...">
                                        <input maxlength="15" v-model="data.quantity" type="text" class="form-control"
                                            placeholder="Add Quantity...">
                                        <div class="input-group-append">
                                            <button title="submit" class="btn btn-outline-secondary bg-white"
                                                type="submit"><i class="mdi mdi-plus"></i></button>
                                        </div>
                                    </form>
                                </template>
                            </ModalCard>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, watchEffect, ref } from 'vue';
import Pop from '../utils/Pop';
import { favoritesService } from '../services/FavoritesService';
import ModalCard from './ModalCard.vue';
import { ingredientsService } from '../services/IngredientsService'
import { Modal } from 'bootstrap';
import { recipesService } from '../services/RecipesService'
import { logger } from '../utils/Logger';

export default {
    setup() {
        let isLocked = reactive({ locked: true })
        const data = ref({})
        watchEffect(() => {
            AppState.activeRecipe
            getIngredients(),
                data.value = { instructions: AppState.activeRecipe.instructions }
            isLocked.locked = true
        })
        async function getIngredients() {
            try {
                await ingredientsService.getIngredients(AppState.activeRecipe.id)
            } catch (error) {
                Pop.error(error)
            }
        }
        return {
            isLocked,
            data,
            recipe: computed(() => AppState.activeRecipe),
            account: computed(() => AppState.account),
            favorites: computed(() => AppState.favorites),
            ingredients: computed(() => AppState.activeIngredients),
            unlock() {
                isLocked.locked = false
                // logger.log(isLocked)
            },
            async addIngredient() {
                try {
                    const newIngredient = {
                        quantity: data.value.quantity,
                        name: data.value.name,
                        recipeId: this.recipe.id
                    }
                    await recipesService.addIngredient(newIngredient)
                    data.value.quantity = ''
                    data.value.name = ''
                } catch (error) {

                }
            },
            async saveInstructions() {
                try {
                    // logger.log(data.value)
                    if (this.recipe.instructions == data.value.instructions) {
                        isLocked.locked = true
                        return
                    }
                    await recipesService.saveInstructions(data.value.instructions, this.recipe.id)
                    isLocked.locked = true
                } catch (error) {
                    Pop.error(error)
                }
            },
            async deleteIngredient(ingredientId) {
                try {
                    const yes = await Pop.confirm('Are you sure you would like to delete this ingredient?')
                    if (!yes) {
                        return
                    }
                    await ingredientsService.deleteIngredient(ingredientId)
                } catch (error) {
                    Pop.error(error)
                }
            },
            async deleteRecipe(recipeId) {
                try {
                    const yes = await Pop.confirm('Are you sure you want to delete your recipe?')
                    if (!yes) {
                        return
                    }
                    await recipesService.deleteRecipe(recipeId)
                    Modal.getOrCreateInstance('#RecipeDetailsModal').hide()
                } catch (error) {
                    Pop.error(error)
                }
            },
            async unFavorite(recipeId) {
                try {
                    const favorite = this.favorites.find(f => f.id == recipeId);
                    // logger.log(AppState.favorites[0])
                    // logger.log(recipeId)
                    await favoritesService.unFavorite(favorite.favoriteId);
                }
                catch (error) {
                    Pop.error(error);
                }
            },
            async favorite(recipeId) {
                try {
                    await favoritesService.favorite(recipeId);
                }
                catch (error) {
                    Pop.error(error);
                }
            }
        };
    },
    components: { ModalCard }
};
</script>


<style lang="scss" scoped>
.overflow {
    height: 287px;
    overflow: scroll;
    overflow-x: hidden;
}

.position {
    position: absolute;
    width: 67%;
    top: 7rem;
}

.bg-green {
    background-color: #527360;
}

textarea {
    width: 100%;
    height: 100%;
    resize: none;
    background-color: #F0F4F2;
    border: none;

}

.height {
    height: 88%;
}




.x-position {
    position: absolute;
    top: 0%;
    right: 1%;
    color: #EB5757;
}

.trash-position {
    position: absolute;
    top: 0%;
    right: 95.7%;
    color: red;
}

.modal-card {
    width: 50%;
    height: 100%;
}

.bg-gray {
    background-color: #BBBBBB;
}

.bg-blur {
    background-color: rgba(0, 0, 0, 0.267);
    backdrop-filter: blur(5px);
}

.modal-height {
    height: 548px;
}

.div-width {
    height: 100%;
    width: 765px;
}

.picture-style {
    height: 100%;
    width: 375px;
    object-fit: cover;
    object-position: center;
}
</style>