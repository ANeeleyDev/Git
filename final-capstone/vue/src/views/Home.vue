<template>
  <div class="home">
    <h1>Home</h1>
    <p>You must be authenticated to see this</p>
    
    <pet-list />
  </div>
</template>

<script>
import PetList from './PetList.vue';

export default {
  components: { PetList },
  name: "home",


};
</script>
