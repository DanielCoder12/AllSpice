<template>
    <!-- TODO MAKE LOOK MORE LIKE MOCK -->
    <div class="m-4 ">

        <div class="hard-shadow egg-img img-fluid rounded"
            :style="{ backgroundImage: 'url(https://i0.wp.com/www.faithanddoubt.com/wp-content/uploads/hossein-farahani-pqJ21tErTgI-unsplash-edited-scaled.jpg?fit=2560%2C1437&ssl=1)', }">
            <div class="d-flex p-3 justify-content-end">
                <!-- TODO MAKE SEARCH WORK -->
                <form @submit.prevent="searchHomePage()" class="d-flex" action="">
                    <input v-model="search" placeholder="Search..." type="text" class="form-control" id="search">
                    <button type="submit" class="btn btn-white"><i class="mdi mdi-magnify"></i></button>
                </form>
                <div class="ps-3">
                    <Login />

                </div>
            </div>
            <div class="text-center custom-font text-white">
                <h1>All-Spice</h1>
                <p class="mb-1">Cherish Your Family</p>
                <p>And Their Cooking</p>
            </div>
        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import Login from './Login.vue';
import { recipesService } from '../services/RecipesService';
export default {
    setup() {
        const search = ref('')
        return {
            search,
            searchHomePage() {
                recipesService.searchHomePage(search.value)
            }
        };
    },
    components: { Login }
};
</script>


<style lang="scss" scoped>
.egg-img {
    height: 20rem;
    width: 100%;
    background-size: cover;

}

.hard-shadow {
    box-shadow: 00px 10px 10px rgba(0, 0, 0, 0.31);
}
</style>