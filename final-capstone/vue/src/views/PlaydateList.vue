<template>
  <div>
      
      <v-row v-for="playdate in playdates" v-bind:key="playdate.playdateId">
          <playdate-card v-bind:playdate = "playdate" />
      
      </v-row>
  </div>
</template>

<script>
import playdateService from '@/services/PlaydateService';
import PlaydateCard from '@/components/PlaydateCard';
export default {
  components: { PlaydateCard },
    name: 'playdate-list',
   
    data(){
        return {
            playdates: {},
            currentUserId: this.$store.state.user.userId,
        }
    },
    methods: {
        getAllPlaydates(){            
            playdateService.getAllPlaydatesForDisplay()
            .then(response => {
          this.playdates = response.data;          
        })
        }
    },
    created(){
        this.getAllPlaydates();
    }

}
</script>

<style>

</style>