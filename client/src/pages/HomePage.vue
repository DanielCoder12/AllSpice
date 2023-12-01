<template>
  <div class="container-fluid">
    <section class="row">
      <!-- header -->
      <div class="col-12">
        <HeaderCard />
      </div>
      <!-- filter button -->
      <div class="col-12 d-flex justify-content-center">
        <FilterButton />
      </div>
      <!-- recipes -->
      <div class="col-12">
        <section class="row">
          <div class="col-4 p-5" v-for="recipe in recipes" :key="recipe.id">

            <RecipeCard :recipe="recipe" />


          </div>
        </section>
      </div>



    </section>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue';
import FilterButton from '../components/FilterButton.vue';
import HeaderCard from '../components/HeaderCard.vue';
import { AppState } from '../AppState';
import { recipesService } from '../services/RecipesService'
import RecipeCard from '../components/RecipeCard.vue';

export default {
  setup() {
    onMounted(() => {
      getRecipes()
    })
    async function getRecipes() {
      await recipesService.getRecipes()
    }
    return {
      recipes: computed(() => AppState.recipes),
    };
  },
  components: { HeaderCard, FilterButton, RecipeCard }
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: clamp(500px, 50vw, 100%);

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }

  // .filter-position {
  //   position: relative;
  //   top: -2.7rem;
  // }
}
</style>
