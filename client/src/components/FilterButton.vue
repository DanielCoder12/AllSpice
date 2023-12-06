<template>
    <div class="rounded filter-position bg-white d-flex shadow">
        <div class="d-flex" v-for="f in filters" :key="f">
            <button @click="changeFilter(f)" :class="{ 'bg-gray': f == filter.toString() }"
                class="btn custom-font text-grn fs-5 px-5 py-3">{{ f }}</button>

        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import { logger } from '../utils/Logger';
import { recipesService } from '../services/RecipesService';
export default {
    setup() {
        const filters = ["Home", "My Recipes", "Favorites"]
        return {
            filters,
            filter: computed(() => AppState.filter),
            changeFilter(filter) {
                recipesService.changeFilter(filter)
            }
        }
    }
};
</script>


<style lang="scss" scoped>
.text-grn {
    color: #219653;
}

.bg-gray {
    background-color: rgba(242, 240, 240, 0.938);
}

.text-grn:hover {
    background-color: rgba(242, 240, 240, 0.926);
}

.filter-position {
    position: relative;
    top: -3.5rem;
}
</style>