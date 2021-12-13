<template>
 <div>
      <v-container>
          <router-link v-bind:to="{name:'add-pet'}"> <v-btn>ADD A PET</v-btn>  </router-link>
      </v-container>
    <v-container>
      <pet-list />
    </v-container>
  </div>
</template>

<script>
import PetList from "./PetList.vue";
export default {
  name: "pet-view",
  components: {
    PetList,
  },
};
</script>

<style>
</style>