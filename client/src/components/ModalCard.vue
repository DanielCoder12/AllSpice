<template>
    <div class="modal-card  p-4 pb-0">
        <div class="h-100 d-flex shadow rounded flex-column">

            <div class="text-center fs-5 text-white p-2 rounded-top bg-green">
                <slot name="title"></slot>
            </div>
            <!-- FIXME height broke after moving to component -->
            <div class="bg-grey p-2 flex-grow-1 rounded-bottom d-flex flex-column justify-content-between">
                <slot name="body"></slot>
                <slot name="form"></slot>
            </div>
        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
export default {
    setup() {
        return {
            recipe: computed(() => AppState.activeRecipe),
        }
    }
};
</script>


<style lang="scss" scoped>
.bg-green {
    background-color: #527360;
}

.bg-grey {
    background-color: #F0F4F2;
}

.modal-card {
    width: 50%;
    height: 100%;
}
</style>