<template>
    <div v-if="user.isAuthenticated" class="d-flex justify-content-end  relative-button">
        <!-- FIXME MAKE NOT DISAPPEAR ON HOVER -->
        <button data-bs-toggle="modal" data-bs-target="#recipeModal" role="button"
            class="btn shadow button-color rounded-circle">
            <i class="mdi mdi-plus text-white fs-1 p-2"></i>
        </button>
    </div>
    <div v-else class="d-flex justify-content-end  relative-button">
        <!-- FIXME MAKE NOT DISAPPEAR ON HOVER -->
        <button @click="login" role="button" class="btn shadow button-color rounded-circle">
            <i class="mdi mdi-plus text-white fs-1 p-2"></i>
        </button>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { AuthService } from '../services/AuthService';
export default {
    setup() {
        return {
            user: computed(() => AppState.user),
            async login() {
                AuthService.loginWithPopup()
            }
        }
    }
};
</script>


<style lang="scss" scoped>
.relative-button {
    position: sticky;
    top: 85dvh;
    height: 5rem;
    z-index: 1050;
}

.button-color {
    background-color: #527360;
    aspect-ratio: 1/1;
}
</style>