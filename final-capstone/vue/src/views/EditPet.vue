<template>
<div>
  <h1>Edit Pet</h1>
  <pet-form v-bind:petId="this.petId" />
      
  </div>
</template>

<script>
import PetForm from "../components/PetForm.vue";
export default {
  props:['petId'],  
  components: { PetForm },
};
</script>

<style>
</style>