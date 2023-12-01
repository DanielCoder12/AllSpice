<template>
    <div class="background-img shadow d-flex flex-column justify-content-between  rounded"
        :style="{ backgroundImage: `url(${recipe.img})` }">
        <div class="d-flex text-white justify-content-between">
            <div class="d-flex m-3 rounded-pill bg-dark px-3">
                <p class="fs-4 mb-0 py-1 px-2">{{ recipe.category }}</p>
            </div>
            <div class=" ">
                <!-- TODO MAKE THIS WORK -->
                <p class="mb-0 px-2 me-3 bg-dark rounded-bottom ">
                    <i class="mdi mdi-heart text-danger fs-3"></i>
                </p>
            </div>
        </div>
        <div class="">
            <div class="bg-dark rounded m-3 px-2 py-1">
                <p class="text-white mb-1 fw-bold">{{ recipe.title }}</p>
                <p class="text-white mb-0"> {{ recipe.instructions }}</p>

            </div>
        </div>

    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Recipe } from '../models/Recipe';
export default {
    props: { recipe: { type: Recipe } },
    setup() {

        return {}
    }
};
</script>


<style lang="scss" scoped>
.background-img {
    height: 30rem;
    width: 100%;
}
</style>