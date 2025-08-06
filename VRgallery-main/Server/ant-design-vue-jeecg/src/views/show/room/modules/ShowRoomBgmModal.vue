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
    <show-room-bgm-form ref="realForm" @ok="submitCallback" :disabled="disableSubmit"></show-room-bgm-form>
  </j-modal>
</template>

<script>

  import ShowRoomBgmForm from './ShowRoomBgmForm'
  export default {
    name: 'ShowRoomBgmModal',
    components: {
      ShowRoomBgmForm
    },
    data () {
      return {
        title:'',
        width:600,
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