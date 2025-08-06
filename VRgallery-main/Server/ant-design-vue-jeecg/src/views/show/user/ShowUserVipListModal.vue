<template>
  <j-modal
    :title="title"
    :width="width"
    :visible="visible"
    @ok="handleCancel"
    :okButtonProps="{ class:{'jee-hidden': disableSubmit} }"
    @cancel="handleCancel"
    >
    <show-user-vip-list ref="realForm" :disabled="disableSubmit"></show-user-vip-list>
  </j-modal>
</template>

<script>

  import ShowUserVipList from '../uservip/ShowUserVipList'
  export default {
    name: 'ShowUserClickLogListModal',
    components: {
      ShowUserVipList
    },
    data () {
      return {
        title:'',
        width:1200,
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