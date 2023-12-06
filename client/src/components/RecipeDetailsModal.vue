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
                                    title="delete" class="mdi trash-position bg-blur rounded-bottom fs-1 mdi-delete"></i>
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
                                <h2 class="mb-0">{{ recipe.title }}</h2>
                                <p class="bg-gray rounded-pill px-2 text-white mb-0">{{ recipe.category }}</p>
                            </div>
                            <p>{{ recipe.creator.name }}'s recipe</p>
                        </div>
                        <div class="d-flex h-75">
                            <ModalCard>
                                <template #title>
                                    <p class="mb-0">Recipe Steps</p>
                                </template>
                                <template #body>
                                    <!-- FIXME ADD EDIT BUTTON THAT TAKES OFF DISABLED PROPERTY -->
                                    <!-- leave it disabled until user clicks edit -->
                                    <form v-if="recipe.creatorId == account.id" @submit.prevent="saveInstructions()">
                                        <textarea v-model="data.instructions" class="rounded" name=""
                                            id="">{{ recipe.instructions }}</textarea>
                                        <!-- <button @click="unlock()" v-if="isLocked == true" type="button"
                                            class="btn btn-secondary">Edit</button> -->
                                        <button type="submit" class="btn btn-success">Save</button>
                                    </form>
                                    <textarea v-else disabled name="" id=""
                                        class="rounded"> {{ recipe.instructions }}</textarea>
                                </template>
                                <!-- <template #form>form</template> -->
                            </ModalCard>
                            <!-- FIXME FIX THE FORM SO YOU CAN ADD AND DELETE INGREDIENTS -->
                            <ModalCard>
                                <template #title>
                                    <p class="mb-0">Ingredients</p>
                                </template>
                                <template #body>
                                    <div v-if="!ingredients[0]?.name">
                                        no ingredients
                                    </div>
                                    <div class="d-flex" v-else v-for="ingredient in ingredients" :key="ingredient.id">
                                        <p>
                                            {{ ingredient.quantity }}
                                        </p>
                                        <p>
                                            {{ ingredient.name }}
                                        </p>
                                    </div>
                                    <!-- <div v-else>
                                        <p>{{ ingredients[0]?.name }}</p>
                                    </div> -->
                                </template>
                                <template #form>
                                    <!-- FIXME MAKE LOOK BETTER -->
                                    <form v-if="recipe.creatorId == account.id" @submit.prevent="addIngredient()"
                                        class="d-flex">

                                        <input v-model="data.name" type="text" class="form-control"
                                            placeholder="Add Ingredients...">
                                        <input v-model="data.quantity" type="text" class="form-control"
                                            placeholder="Add Quantity...">
                                        <div class="input-group-append">
                                            <button class="btn btn-outline-secondary bg-white" type="submit"><i
                                                    class="mdi mdi-plus"></i></button>
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
        let isLocked = true
        const data = ref({})
        watchEffect(() => {
            AppState.activeRecipe
            getIngredients(),
                data.value = { instructions: AppState.activeRecipe.instructions }
            isLocked = true
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
                isLocked = false
                logger.log(isLocked)
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
                    logger.log(data.value)
                    await recipesService.saveInstructions(data.value.instructions, this.recipe.id)
                    isLocked = true
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
textarea {
    width: 100%;
    height: 100%;
    resize: none;
    background-color: #F0F4F2;
    border: none;

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
}
</style>