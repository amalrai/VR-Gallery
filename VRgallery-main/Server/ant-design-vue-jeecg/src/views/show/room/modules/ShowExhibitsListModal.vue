<template>
  <j-modal
    :title="title"
    :width="width"
    :visible="visible"
    @ok="handleCancel"
    :okButtonProps="{ class:{'jee-hidden': disableSubmit} }"
    @cancel="handleCancel"
    fullscreen>
    <show-exhibits-list ref="realForm" :disabled="disableSubmit"></show-exhibits-list>
  </j-modal>
</template>

<script>

  import ShowExhibitsList from '../../exhibits/ShowExhibitsList'
  export default {
    name: 'ShowExhibitsListModal',
    components: {
      ShowExhibitsList
    },
    data () {
      return {
        title:'',
        width:800,
        visible: false,
        disableSubmit: false
      }
    },
    methods: {
      open (record) {
        this.visible=true
        this.$nextTick(()=>{
          this.$refs.realForm.open(record);
        })
      },
      close () {
        this.$emit('close');
        this.visible = false;
      },
      handleCancel () {
        this.close()
      }
    }
  }
</script>