<template>
    <div class="d-flex justify-content-end  relative-button">
        <!-- FIXME MAKE NOT DISAPPEAR ON HOVER -->
        <button data-bs-toggle="modal" data-bs-target="#exampleModal" role="button"
            class="btn shadow button-color rounded-circle">
            <i class="mdi mdi-plus text-white fs-1 p-2"></i>
        </button>
    </div>
    <ReuseableModal />
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
export default {
    setup() {
        return {}
    }
};
</script>


<style lang="scss" scoped>
.relative-button {
    position: sticky;
    top: 53rem;
    height: 5rem;
    z-index: 1050;
}

.button-color {
    background-color: #527360;
}
</style>