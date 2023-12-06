<template>
  <header>

  </header>
  <main>
    <create-recipe-button />
    <div class="offset">
      <router-view />
    </div>
  </main>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'
import CreateRecipeButton from './components/CreateRecipeButton.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar, CreateRecipeButton }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}


footer {
  display: grid;
  place-content: center;
  height: 32px;
}

.custom-font {
  font-family: 'Bebas Neue', sans-serif;
  font-family: 'Sahitya', serif;
}


.offset {
  position: relative;
  top: -5rem;
}
</style>
