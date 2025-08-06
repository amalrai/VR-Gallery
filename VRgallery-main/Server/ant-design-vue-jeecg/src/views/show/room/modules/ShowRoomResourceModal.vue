<template>
  <j-modal
    :title="title"
    :width="width"
    :visible="visible"
    switchFullscreen
    @ok="handleOk"
    :okButtonProps="{ class:{'jee-hidden': disableSubmit} }"
    @cancel="handleCancel"
    :cancelText="$t('关闭')"
    :okText="$t('确定')">
    <show-room-resource-form ref="realForm" @ok="submitCallback" :disabled="disableSubmit"></show-room-resource-form>
  </j-modal>
</template>

<script>

  import ShowRoomResourceForm from './ShowRoomResourceForm'
  export default {
    name: 'ShowRoomResourceModal',
    components: {
      ShowRoomResourceForm
    },
    data () {
      return {
        title:'',
        width:1400,
        visible: false,
        disableSubmit: false
      }
    },
    methods: {
      edit (roomId) {
        this.visible=true
        this.$nextTick(()=>{
          this.$refs.realForm.edit(roomId);
        })
      },
      close () {
        this.$emit('close');
        this.visible = false;
      },
      handleOk () {
        this.$refs.realForm.submitForm();
      },
      submitCallback(){
        this.$emit('ok');
        this.visible = false;
      },
      handleCancel () {
        this.close()
      }
    }
  }
</script>