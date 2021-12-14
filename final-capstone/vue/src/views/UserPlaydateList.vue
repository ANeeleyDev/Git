<template>
  <div>
      <v-row>
      <v-col v-for="playdate in playdates" v-bind:key="playdate.playdateId">
          <playdate-card v-bind:playdate = "playdate" />
      </v-col>
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
        getUserPlaydates(){            
            playdateService.getLoggedInUserPlaydates()
            .then(response => {
          this.playdates = response.data;          
        })
        }
    },
    created(){
        this.getUserPlaydates();
    }

}
</script>

<style>

</style>