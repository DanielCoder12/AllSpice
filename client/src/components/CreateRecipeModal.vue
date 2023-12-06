<template>
    <div class="modal fade" id="recipeModal" tabindex="-1" aria-labelledby="recipeModal" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header py-1 bg-green">
                    <h5 class="modal-title custom-font" id="recipeModal">New Recipe</h5>
                    <button type="button" class="btn fs-3 text-white mdi mdi-close" data-bs-dismiss="modal"
                        aria-label="Close"></button>
                </div>
                <form @submit.prevent="createRecipe()">
                    <div class="modal-body">

                        <div class="container-fluid">
                            <section class="row">
                                <div class="col-7 pe-4">
                                    <div class="mb-3">
                                        <label for="title" class="form-label">Title</label>
                                        <input maxlength="50" required v-model="formData.title" placeholder="Title..."
                                            type="text" class="form-control" id="title">
                                    </div>
                                </div>
                                <div class="col-5 ps-0">
                                    <div>
                                        <label placeholder="Appetizer..." for="category" class="form-label">Category</label>
                                        <select v-model="formData.category" class="form-select" id="category" required>
                                            <option v-for="category in categories" :key="category">{{ category }}</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="mb-3">
                                        <label for="img" class="form-label">Image Url</label>
                                        <input maxlength="500" required v-model="formData.img" placeholder="Image Url..."
                                            type="url" class="form-control" id="img">
                                    </div>
                                    <div class="mb-3">
                                        <label for="instructions" class="form-label">Sub Title</label>
                                        <input maxlength="50" required v-model="formData.instructions"
                                            placeholder="Details..." type="text" class="form-control" id="instructions">
                                        <div id="instructions" class="form-text">A brief description of the recipe
                                        </div>
                                    </div>
                                </div>
                            </section>
                        </div>
                    </div>
                    <div class="d-flex justify-content-end">
                        <div class=" p-3">
                            <button type="button" class="btn me-3" data-bs-dismiss="modal">Cancel</button>
                            <button type="submit" class="btn bg-green">Save changes</button>

                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import Pop from '../utils/Pop';
import { recipesService } from '../services/RecipesService';
import { logger } from '../utils/Logger';
import { Modal } from 'bootstrap';
export default {
    setup() {
        const formData = ref({})
        const categories = ['Cheese', 'Italian', 'Mexican', 'Soup', 'Specialty Coffee']
        return {
            formData,
            categories,
            async createRecipe() {
                try {
                    // logger.log(formData.value)
                    await recipesService.createRecipe(formData.value)
                    Modal.getOrCreateInstance('#recipeModal').hide()
                    formData.value = {}
                } catch (error) {
                    Pop.error(error)
                }
            }
        }
    }
};
</script>


<style lang="scss" scoped>
.bg-green {
    background-color: #527360;
    color: white;
}
</style>