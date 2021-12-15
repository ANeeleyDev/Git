<template>
<div>
  <h1>Edit Playdate</h1>
  <playdate-form v-bind:playdateId="this.playdateId" />
      
  </div>
</template>

<script>
import PlaydateForm from "../components/PlaydateForm.vue";
export default {
  props:['playdateId'],  
  components: { PlaydateForm },
};
</script>

<style>
</style>