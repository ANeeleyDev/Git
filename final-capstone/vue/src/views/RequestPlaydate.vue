<template>
<div>
  <h1>Request Playdate</h1>
  <request-playdate-form v-bind:playdateId="this.playdateId" />
      
  </div>
</template>

<script>
import RequestPlaydateForm from "../components/RequestPlaydateForm.vue";
export default {
  props:['playdateId'],  
  components: { RequestPlaydateForm },
};
</script>

<style>
</style>