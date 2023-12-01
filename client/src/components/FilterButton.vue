<template>
    <div class="rounded filter-position bg-white d-flex shadow">
        <div class="d-flex" v-for="f in filters" :key="f">
            <button class="btn custom-font text-grn fs-5 px-5 py-3">{{ f }}</button>

        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
export default {
    setup() {
        const filters = ["Home", "My Recipes", "Favorites"]
        return {
            filters
        }
    }
};
</script>


<style lang="scss" scoped>
.text-grn {
    color: #219653;
}

.filter-position {
    position: relative;
    top: -3.5rem;
}
</style>